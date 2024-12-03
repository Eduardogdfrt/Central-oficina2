import React from "react";
import "../../pages/aluno/Home.css"
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
          <p className="text">Faça login e escaneie o QR code gerado pelo professor pra registrar sua presença</p>
        </div>
        </div>
  );
};

export default Home;

