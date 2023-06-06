import React from 'react';
import LoginForm from './components/Auth/LoginForm';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Products from './components/Products/Products';
import Processes from './components/Processes/Processes';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element ={<LoginForm />} />
        <Route path="/products" element={<Products />} />
        <Route path="/processes/:id" element={<Processes />} />
      </Routes>
    </Router>
  );
};

export default App;
