import React from 'react';
import LoginForm from './components/Auth/LoginForm';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Products from './components/Products/Products';
import Processes from './components/Processes/Processes';
import Users from './components/Users/Users';
import Report from './components/Report/Report';
import ProductionProcesses from './components/ProductionProcesses/ProductionProcesses';

const App: React.FC = () => {
  return (
    <Router>
      <Routes>
        <Route path="/" element ={<LoginForm />} />
        <Route path="/products" element={<Products />} />
        <Route path="/processes/:id" element={<Processes />} />
        <Route path="/users" element={<Users />} />
        <Route path="/report" element={<Report />} />
        <Route path="/productionProcesses" element={<ProductionProcesses />} />
      </Routes>
    </Router>
  );
};

export default App;
