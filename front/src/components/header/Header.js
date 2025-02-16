import React from "react";
import { Link, useLocation } from "react-router-dom";
import "./Header.css";
import logo from "../../assets/images/logo.png";
import Title from "../title/Title";

const Header = () => {
  const location = useLocation();

  let titleText = "";
  let linkTo = "";

  switch (location.pathname) {
    case "/":
      titleText = "ENTRAR";
      linkTo = "/login";
      break;
    case "/login":
      titleText = "CADASTRO";
      linkTo = "/cadastro";
      break;
    case "/cadastro":
      titleText = "ENTRAR";
      linkTo = "/login";
      break;
    default:
      titleText = "SAIR";
      linkTo = "/";
  }

  return (
    <header className="header">
      <div className="header-logo">
        <img src={logo} alt="Logo" className="img-logo" />
      </div>
      <Link to={linkTo} className="header-link">
        <Title text={titleText} fontSize="1.6rem" />
      </Link>
    </header>
  );
};

export default Header;
