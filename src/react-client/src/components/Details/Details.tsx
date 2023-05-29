import React, { useEffect, useState } from "react";
import Button from "@mui/material/Button";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TablePagination from "@mui/material/TablePagination";
import AddDetailDialog from "../Dialogs/AddDetailDialog/AddDetailDialog";
import EditDetailDialog from "../Dialogs/EditDetailDialog/EditDetailDialog";

interface Detail {
    id: number;
    name: string;
}
  
interface ApiResponse {
    items: Detail[];
    pageNumber: number;
    totalPages: number;
    totalCount: number;
    hasPreviousPage: boolean;
    hasNextPage: boolean;
}

const Details: React.FC = () => {
  const [editedDetail, setEditedDetail] = useState<Detail | null>(null);
  const [addDialogOpen, setAddDialogOpen] = useState(false);
  const [editDialogOpen, setEditDialogOpen] = useState(false);
  const [details, setDetails] = useState<Detail[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [page, setPage] = useState<number>(() => {
    const savedPage = localStorage.getItem("savedPage");
    return savedPage ? parseInt(savedPage, 10) : 1;
  });

  const handleAddDialogOpen = () => {
    setAddDialogOpen(true);
  };
  
  const handleAddDialogClose = () => {
    setAddDialogOpen(false);
  };
  
  const handleEditDialogOpen = (detail: Detail) => {
    setEditedDetail(detail);
    setEditDialogOpen(true);
  };
  
  const handleEditDialogClose = () => {
    setEditedDetail(null);
    setEditDialogOpen(false);
  };

  useEffect(() => {
    fetch("http://localhost:7115/api/details")
      .then((response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch details");
        }
        return response.json();
      })
      .then((data: ApiResponse) => {
        setDetails(data.items);
        setTotalPages(data.totalPages);})
      .catch((error) => console.error("Error fetching details:", error));
  }, []);

  const goToPage = (pageNumber: number) => {
    fetch(`http://localhost:7115/api/details?pageNumber=${pageNumber}`)
      .then((response) => {
        if (!response.ok) {
          throw new Error("Failed to fetch details");
        }
        return response.json();
      })
      .then((data: ApiResponse) => {
        setDetails(data.items);
        setTotalPages(data.totalPages);
        setCurrentPage(pageNumber);
      })
      .catch((error) => console.error("Error fetching details:", error));

      setPage(pageNumber);
      localStorage.setItem("savedPage", pageNumber.toString());
  };

  const handleDelete = (id: number) => {
    fetch(`http://localhost:7115/api/details/${id}`, {
      method: "DELETE",
    })
      .then((response) => {
        if (!response.ok) {
          throw new Error("Failed to delete detail");
        }
        // Удалите деталь из состояния
        setDetails(details.filter((detail) => detail.id !== id));
      })
      .catch((error) => console.error("Error deleting detail:", error));
  };

  return (
    <div>
        <h2>Details</h2>

<div>
<Button onClick={handleAddDialogOpen} variant="contained" color="primary">
  Add Detail
</Button>
    <AddDetailDialog
        open={addDialogOpen}
        onClose={handleAddDialogClose}
    />
    </div>

    <EditDetailDialog
  open={editDialogOpen}
  onClose={handleEditDialogClose}
  editedDetail={editedDetail}
/>

<TablePagination
  component="div"
  count={-1}
  page={currentPage - 1}
  onPageChange={(_, page) => goToPage(page + 1)}
  rowsPerPage={10}
  onRowsPerPageChange={() => {}}
/>

<Table>
  <TableHead>
    <TableRow>
      <TableCell>ID</TableCell>
      <TableCell>Name</TableCell>
      <TableCell>Actions</TableCell>
    </TableRow>
  </TableHead>
  <TableBody>
    {details.map((detail) => (
      <TableRow key={detail.id}>
        <TableCell>{detail.id}</TableCell>
        <TableCell>{detail.name}</TableCell>
        <TableCell>
        <Button onClick={() => handleEditDialogOpen(detail)} variant="contained">
            Edit
        </Button>

          <Button
            onClick={() => handleDelete(detail.id)}
            variant="contained"
            color="error">
            Delete
          </Button>
        </TableCell>
      </TableRow>
    ))}
  </TableBody>
</Table>

</div>
  );
};

export default Details;