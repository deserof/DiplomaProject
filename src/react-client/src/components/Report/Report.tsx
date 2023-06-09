import React, { useEffect, useState } from 'react';
import MainMenu from '../Menu/MainMenu';
import { Container, Typography } from '@mui/material';

const Report: React.FC = () => {
    return (
        <Container>
            <MainMenu />
            <Typography variant="h4" gutterBottom>
                Отчет
            </Typography>
        </Container>
    );
}

export default Report;