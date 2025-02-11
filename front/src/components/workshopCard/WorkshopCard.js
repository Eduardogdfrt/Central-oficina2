import React from "react";
import "./WorkshopCard.css";

const WorkshopCard = ({ text, link, icon }) => {
  return (
    <div className="card-container">
      <div className="card">
        <img src={icon} alt={text} className="icon-card" />
      </div>
      <p className="text-card">{text}</p>
    </div>
  );
};

export default WorkshopCard;
