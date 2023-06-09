import React, { useEffect, useState } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Typography,
    Container,
} from '@mui/material';
import { getUsers } from '../../common/apiService';
import { User, UsersResponse } from '../../common/types';
import { TablePagination } from '@mui/material';
import MainMenu from '../Menu/MainMenu';

const Users: React.FC = () => {
    const [users, setUsers] = useState<User[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [totalPages, setTotalPages] = useState<number>(0);

    const [rowsPerPage, setRowsPerPage] = useState<number>(10);

    const handleChangePage = (
        event: React.MouseEvent<HTMLButtonElement> | null,
        newPage: number
    ) => {
        setCurrentPage(newPage + 1);
    };

    const handleChangeRowsPerPage = (
        event: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
    ) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setCurrentPage(1);
    };

    useEffect(() => {
        const fetchUsers = async () => {
            const pageSize = 10;
            const response: UsersResponse = await getUsers(currentPage, pageSize);
            setUsers(response.items);
            setTotalPages(response.totalPages);
        };

        fetchUsers();
    }, [currentPage]);

    return (
        <Container>
            <MainMenu />
            <Typography variant="h4" gutterBottom>
                Список пользователей
            </Typography>

            <TablePagination
                component="div"
                count={totalPages * rowsPerPage}
                page={currentPage - 1}
                onPageChange={handleChangePage}
                rowsPerPage={rowsPerPage}
                onRowsPerPageChange={handleChangeRowsPerPage}
                labelRowsPerPage="Строк на странице:"
            />
            <TableContainer component={Paper}>
                <Table>
                    <TableHead>
                        <TableRow>
                            <TableCell>ID</TableCell>
                            <TableCell>Имя</TableCell>
                            <TableCell>Фамилия</TableCell>
                            <TableCell>Должность</TableCell>
                            <TableCell>Дата приема на работу</TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {users.map((user) => (
                            <TableRow key={user.id}>
                                <TableCell>{user.id}</TableCell>
                                <TableCell>{user.firstName}</TableCell>
                                <TableCell>{user.lastName}</TableCell>
                                <TableCell>{user.position}</TableCell>
                                <TableCell>
                                    {new Date(user.hireDate).toLocaleDateString()}
                                </TableCell>
                            </TableRow>
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
        </Container>
    );
};

export default Users;

