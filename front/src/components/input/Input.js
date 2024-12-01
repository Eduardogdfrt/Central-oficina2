import React from 'react';
import PropTypes from 'prop-types';
import './Input.css'; 

const CustomInput = ({
  type = 'text',
  placeholder = '',
  value = '',
  onChange,
  customStyle = {},
}) => {
  return (
    <input
      type={type}
      placeholder={placeholder}
      value={value}
      onChange={onChange}
      style={customStyle}
      className="custom-input"
    />
  );
};


CustomInput.propTypes = {
  type: PropTypes.oneOf(['email', 'password', 'text']), 
  placeholder: PropTypes.string, 
  value: PropTypes.string, 
  onChange: PropTypes.func.isRequired, 
  customStyle: PropTypes.object, 
};

export default CustomInput;
