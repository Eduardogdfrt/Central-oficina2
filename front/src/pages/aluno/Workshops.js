import React from "react";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import card01 from "../../assets/images/card01.png";
import card02 from "../../assets/images/card02.png";
import card03 from "../../assets/images/card03.png";
import WorkshopCard from "../../components/workshopCard/WorkshopCard";
import "../../pages/aluno/Workshop.css";

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
        </div>
      </div>
    </div>
  );
};

export default Workshops;
