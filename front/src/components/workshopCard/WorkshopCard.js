import React from "react";
import "./WorkshopCard.css";

const WorkshopCard = ({ image, text }) => {
  return (
    <div className="card-container">
      <div className="card">
        <img src={image} alt="workshop" className="img-card" />
      </div>
      <p className="text-card">{text}</p>
    </div>
  );
};

export default WorkshopCard;
