import React from 'react';
<<<<<<< Updated upstream
import { useNavigate } from 'react-router-dom';
import './Button.css'; 

const Button = ({ text, onClick }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    if (onClick) {
      onClick(); 
    }
    navigate('/outra-pagina');
  };

  return (
    <button className="button" onClick={handleClick}>
=======
import './Button.css'; 

const Button = ({ text, onClick }) => {
  return (
    <button className="button" onClick={onClick}>
>>>>>>> Stashed changes
      {text}
    </button>
  );
};

export default Button;
