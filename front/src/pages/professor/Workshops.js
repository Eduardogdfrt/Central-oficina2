import React, { useState, useEffect } from "react";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import WorkshopCard from "../../components/workshopCard/WorkshopCard";
import "../../pages/professor/Workshop.css";
import axios from "axios";

const Workshops = () => {
  const [workshops, setWorkshops] = useState([]);

  useEffect(() => {
    // Função para buscar workshops do backend
    const fetchWorkshops = async () => {
      try {
        const response = await axios.get("http://localhost:5000/api/Workshop");
        setWorkshops(response.data);
      } catch (error) {
        console.error("Erro ao buscar workshops:", error);
      }
    };

    fetchWorkshops();
  }, []);

  return (
    <div className="page">
      <Header title="SAIR" />
      <div className="content">
        <Title text="MEUS WORKSHOPS" fontSize="3.5rem" />
        <div className="content-workshop">
          {workshops.map((workshop) => (
            <WorkshopCard key={workshop.id} text={workshop.name} link={`/workshop/${workshop.id}`} />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Workshops;
