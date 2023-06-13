import { Button, Container, TextField, Typography } from '@mui/material';
import { downloadExcel } from '../../common/apiService';
import { useState } from 'react';
import MainMenu from '../Menu/MainMenu';

const Report: React.FC = () => {
  const [fromDate, setFromDate] = useState<Date>(new Date());
  const [toDate, setToDate] = useState<Date>(new Date());

  return (
    <Container>
      <MainMenu />
      <Typography variant="h4" gutterBottom>
        Отчет
      </Typography>
      <TextField
        margin="dense"
        label="С"
        type="datetime-local"
        fullWidth
        InputLabelProps={{
          shrink: true,
        }}
        value={fromDate.toISOString().slice(0, 16)}
        onChange={(e) => setFromDate(new Date(e.target.value))}
      />
      <TextField
        margin="dense"
        label="По"
        type="datetime-local"
        fullWidth
        InputLabelProps={{
          shrink: true,
        }}
        value={toDate.toISOString().slice(0, 16)}
        onChange={(e) => setToDate(new Date(e.target.value))}
      />
      <Button
        variant="contained"
        color="primary"
        onClick={async () => {
          const excelBlob = await downloadExcel(fromDate, toDate);
          const url = URL.createObjectURL(excelBlob);
          const link = document.createElement('a');
          link.href = url;
          link.download = 'report.xlsx';
          document.body.appendChild(link);
          link.click();
          document.body.removeChild(link);
        }}
      >
        Скачать Excel-файл
      </Button>
    </Container>
  );
};

export default Report;