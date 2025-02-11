import React from "react";
import "../../pages/professor/Home.css"
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Scene from "../../components/Scene"


const Home = () => {
  return (
      <div className="page">
        <Header title="ENTRAR"/>
        <div className="content">
          <Scene/>
          <Title text="QR CODE" fontSize="3.5rem" />
            <p className="text">Gerar um QR code temporário pra confirmar a presença dos alunos nas oficinas</p>
          <div className="glow">
            <p>a</p>
          </div>
        </div>
      </div>
  );
};

export default Home;

