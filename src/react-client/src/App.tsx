import React from 'react';
import LoginForm from './components/Auth/LoginForm';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Products from './components/Products/Products';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element ={<LoginForm />} />
        <Route path="/products" element={<Products />} />
      </Routes>
    </Router>
  );
};

export default App;
