// LoginForm.tsx
import React, { useState, ChangeEvent, FormEvent } from 'react';
import axios from 'axios';
import {
  Button,
  TextField,
  Typography,
  Container,
  Box,
  Alert,
} from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { BASE_URL } from '../../common/constants';

const LoginForm: React.FC = () => {
  const [username, setUsername] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: FormEvent) => {
    e.preventDefault();

    try {
      const response = await axios.post(`${BASE_URL}/connect/token`, new URLSearchParams({
        grant_type: 'password',
        username,
        password,
      }), {
        headers: {
          'Content-Type': 'application/x-www-form-urlencoded',
        },
      });

      localStorage.setItem('access_token', response.data.access_token);
      localStorage.setItem('id_token', response.data.id_token);

      setError(null);
      navigate('/products');
    } catch (err) {
      setError('Ошибка входа');
    }
  };

  const handleInputChange = (
    e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>,
    setter: React.Dispatch<React.SetStateAction<string>>
  ) => {
    setter(e.target.value);
  };

  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          marginTop: 8,
          display: 'flex',
          flexDirection: 'column',
          alignItems: 'center',
        }}
      >
        <Typography component="h1" variant="h5">
          Вход
        </Typography>
        {error && (
          <Box mt={2}>
            <Alert severity="error">{error}</Alert>
          </Box>
        )}
        <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1, width: '100%' }}>
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            id="username"
            label="Имя пользователя"
            name="username"
            autoFocus
            value={username}
            onChange={(e) => handleInputChange(e, setUsername)}
          />
          <TextField
            variant="outlined"
            margin="normal"
            required
            fullWidth
            name="password"
            label="Пароль"
            type="password"
            id="password"
            value={password}
            onChange={(e) => handleInputChange(e, setPassword)}
          />
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Войти
          </Button>
        </Box>
      </Box>
    </Container>
  );
};

export default LoginForm;