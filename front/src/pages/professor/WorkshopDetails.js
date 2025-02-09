import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Button from "../../components/button/Button";
import "../../pages/professor/Workshop.css";

// icones personalizados
import card01 from "../../assets/images/card01.png"; // robótica
import card02 from "../../assets/images/card02.png"; // lógica
import card03 from "../../assets/images/card03.png"; // programação
import card04 from "../../assets/images/card04.png"; // icone padrão

const getWorkshopIcon = (name) => {

  if (name.includes("Robótica")) return card01;
  if (name.includes("Lógica")) return card02;
  if (name.includes("Programação")) return card03;
  return card04; 
};

const WorkshopDetails = () => {
  const { id } = useParams(); 
  const navigate = useNavigate(); 
  const [workshop, setWorkshop] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchWorkshop = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/Workshop/${id}`);
        if (!response.ok) throw new Error("Erro ao carregar dados do workshop");

        const data = await response.json();
        if (data.success) {
          setWorkshop(data.workshop); 
        } else {
          setError("Workshop não encontrado");
        }
      } catch (err) {
        setError("Erro na requisição");
      } finally {
        setLoading(false);
      }
    };

    fetchWorkshop();
  }, [id]);

  if (loading) {
    return <p>Carregando detalhes do workshop...</p>;
  }

  if (error) {
    return <p className="error">{error}</p>;
  }


  const formattedDate = new Date(workshop.data);
  const day = formattedDate.getDate().toString().padStart(2, "0");
  const month = (formattedDate.getMonth() + 1).toString().padStart(2, "0");
  const year = formattedDate.getFullYear();
  const hours = formattedDate.getHours().toString().padStart(2, "0");

  const formattedDateString = `${day}.${month}.${year} - ${hours}H`;

  const handleGenerateCertificate = () => {

    navigate("/certificado", { state: { workshopId: id } });
  };
  
  

  return (
    <div className="page">
      <Header title="Detalhes do Workshop" />
      <div className="content">
        <img
          src={getWorkshopIcon(workshop.name)}
          alt="Workshop Icon"
          className="workshop-icon"
          style={{ margin: "20px" }}
        />
        <div className="workshop-details">
          <Title text={workshop.name} fontSize="3.5rem" />
          <p className="text no-width">Data: {formattedDateString}</p>
        </div>
        <p className="text no-width">
          Deseja validar a presença dos alunos nas oficinas gerando um certificado?
        </p>
         <div className="inputs">
          <Button text="GERAR NOVO CERTIFICADO" onClick={handleGenerateCertificate} width="50%"/>
          </div>
      </div>
    </div>
  );
};

export default WorkshopDetails;
