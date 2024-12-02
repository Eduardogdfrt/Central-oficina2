import React from "react";
import "../../pages/professor/Home.css"
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import qr from "../../assets/images/qr.png";


const Home = () => {
  return (
      <div className="page">
        <Header title="ENTRAR"/>
        <div className="content">
          <img src={qr} alt="qr" height="400px"/>
          <Title text="QR CODE" fontSize="3.5rem" />
          <p className="text">Gerar um QR code temporário pra confirmar a presença dos alunos nas oficinas</p>
        </div>
        </div>
  );
};

export default Home;

