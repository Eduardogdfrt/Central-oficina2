import React, { useEffect } from "react";
import "../../pages/professor/Home.css"
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import done from "../../assets/images/done.png"
import Button from "../../components/button/Button";
import { useLocation } from "react-router-dom";

const Certificado = () => {
  const location = useLocation();
  const token = location.state?.token; 
  
  useEffect(() => {
    if (token) {
      console.log("Token recebido: ", token); 
    }
  }, [token]);

    return (
        <div className="page">
          <Header title="ENTRAR"/>
          <div className="content-workshops">
            <Title text="QR CODE" fontSize="3.5rem" />
            <img src={done} alt="Descrição da imagem" width={400}/>
              <p className="text">
                Certificado emitido com sucesso!
                </p>
                <p className="text">Token do certificado: {token}</p>
                <div className="inputs">
          <Button text="VOLTAR" width="50%"/>
          </div>
          </div>

        </div>
    );
  };
  
  export default Certificado;
  