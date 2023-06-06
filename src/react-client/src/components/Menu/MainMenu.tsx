// MainMenu.tsx
import React from 'react';
import { Button, ButtonGroup } from '@mui/material';
import { useNavigate } from 'react-router-dom';

const MainMenu: React.FC = () => {
    const navigate = useNavigate();
  
    return (
      <ButtonGroup variant="contained" color="primary" aria-label="main menu">
        <Button onClick={() => navigate('/products')}>Продукты</Button>
        <Button onClick={() => navigate('/report')}>Отчет</Button>
        <Button onClick={() => navigate('/processes')}>Производственные процессы</Button>
        <Button onClick={() => navigate('/users')}>Пользователи</Button>
      </ButtonGroup>
    );
  };
  
  export default MainMenu;