import React from "react";
<<<<<<< Updated upstream
import "./WorkshopCard.css";

const WorkshopCard = ({ image, text }) => {
  return (
    <div className="card-container">
=======
import { useNavigate } from "react-router-dom"; // Importa o useNavigate
import "./WorkshopCard.css";

const WorkshopCard = ({ image, text, link }) => {
  const navigate = useNavigate(); // Inicializa o hook de navegação

  const handleNavigation = () => {
    if (link) {
      navigate(link); // Redireciona para a rota passada na prop link
    }
  };

  return (
    <div className="card-container" onClick={handleNavigation}>
>>>>>>> Stashed changes
      <div className="card">
        <img src={image} alt="workshop" className="img-card" />
      </div>
      <p className="text-card">{text}</p>
    </div>
  );
};

export default WorkshopCard;
