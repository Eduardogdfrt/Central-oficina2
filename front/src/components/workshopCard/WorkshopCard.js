import React from "react";
import "./WorkshopCard.css";

const WorkshopCard = ({ text, link }) => {
  return (
    <div className="card-container">
      <div className="card">
        <p className="text-card">{text}</p>
      </div>
      <a href={link} className="card-link">Ver detalhes</a>
    </div>
  );
};

export default WorkshopCard;
