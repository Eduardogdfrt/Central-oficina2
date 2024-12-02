import React from 'react';
<<<<<<< Updated upstream
=======
import { Link, useLocation } from 'react-router-dom';
>>>>>>> Stashed changes
import './Header.css';
import logo from '../../assets/images/logo.png';
import Title from '../title/Title';

<<<<<<< Updated upstream

const Header = ({ title }) => {
  return (
    <header className="header">
      <div className="header-logo">
      <img src={logo} alt="Logo" className="img-logo"/>
      </div>
      <Title text={title} fontSize="1.6rem" />
=======
const Header = () => {
  const location = useLocation(); 

  
  let titleText = '';
  let linkTo = '';

  switch (location.pathname) {
    case '/':
      titleText = 'ENTRAR';
      linkTo = '/login';
      break;
    case '/login':
      titleText = 'CADASTRO';
      linkTo = '/cadastro';
      break;
    case '/cadastro':
      titleText = 'ENTRAR';
      linkTo = '/login';
      break;
    default:
      titleText = 'SAIR';
      linkTo = '/';
  }

  return (
    <header className="header">
      <div className="header-logo">
        <img src={logo} alt="Logo" className="img-logo" />
      </div>
      <Link to={linkTo} className="header-link">
        <Title text={titleText} fontSize="1.6rem" />
      </Link>
>>>>>>> Stashed changes
    </header>
  );
};

export default Header;
