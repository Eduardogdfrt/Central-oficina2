import React from "react";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import card01 from "../../assets/images/card01.png";
import card02 from "../../assets/images/card02.png";
import card03 from "../../assets/images/card03.png";
import card04 from "../../assets/images/card04.png";
import WorkshopCard from "../../components/workshopCard/WorkshopCard";
import { Link } from "react-router-dom";
import "../../pages/professor/Workshop.css";

const Workshops = () => {
  return (
    <div className="page">
      <Header title="SAIR" />
      <div className="content">
        <Title text="MEUS WORKSHOPS" fontSize="3.5rem" />
        <div className="content-workshop">
          <WorkshopCard image={card01} text="Robótica" link="/robotica" />
          <WorkshopCard image={card02} text="Lógica" link="/logica" />
          <WorkshopCard image={card03} text="Programação" link="/programacao" />
           <Link to="/workshop-cadastro" className="workshop-add-button">
            <WorkshopCard image={card04} text="Adicionar" link="/workshop-cadastro" />
          </Link>
        </div>
      </div>
    </div>
  );
};

export default Workshops;
