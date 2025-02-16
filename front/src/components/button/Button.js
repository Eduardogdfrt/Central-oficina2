import React from 'react';
import './Button.css'; 

const Button = ({ text, onClick,  width}) => {
  return (
    <button className="button" onClick={onClick} width={width}>
      {text}
    </button>
  );
};

export default Button;