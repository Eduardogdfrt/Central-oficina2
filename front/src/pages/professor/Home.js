import React from "react";
import "../../pages/professor/Home.css";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Scene from "../../components/Scene";

const Home = () => {
  return (
    <div className="page">
      <Header title="ENTRAR" />
      <div className="content">
        <Scene />
        <Title text="Certificado" fontSize="3.5rem" />
        <p className="text">
          Gere um certificado temporário e confirme a presença na oficina
        </p>
        <div className="glow">
          <p>a</p>
        </div>
      </div>
    </div>
  );
};

export default Home;
