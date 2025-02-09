import React, { useEffect, useState, useRef } from "react";
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import { Link } from "react-router-dom";
import WorkshopCard from "../../components/workshopCard/WorkshopCard";
import "../../pages/professor/Workshop.css";
import { useNavigate } from "react-router-dom"; 

// icones personalizados
import card01 from "../../assets/images/card01.png"; // robótica
import card02 from "../../assets/images/card02.png"; // lógica
import card03 from "../../assets/images/card03.png"; // programação
import card05 from "../../assets/images/card05.png"; // default
const getWorkshopIcon = (name) => {

  if (name.includes("Robótica")) return card01;
  if (name.includes("Lógica")) return card02;
  if (name.includes("Programação")) return card03;
  return card05; 
};

const Workshops = () => {
  const [workshops, setWorkshops] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const carouselRef = useRef(null);

  const navigate = useNavigate();

  const scrollCarousel = (direction) => {
    if (carouselRef.current) {
      const scrollAmount = 300; 
      if (direction === "left") {
        carouselRef.current.scrollLeft -= scrollAmount;
      } else {
        carouselRef.current.scrollLeft += scrollAmount;
      }
    }
  };

  useEffect(() => {
    const fetchWorkshops = async () => {
      try {
        const response = await fetch("http://localhost:5000/api/Workshop/FindAll");
        if (!response.ok) throw new Error("Erro ao carregar workshops");

        const data = await response.json();
        if (data.workshops && Array.isArray(data.workshops)) {
          setWorkshops(data.workshops);
        } else {
          setWorkshops([]);
        }
      } catch (err) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchWorkshops();
  }, []);

  return (
    <div className="page">
      <Header title="SAIR" />
      <div className="content">
        <Title text="MEUS WORKSHOPS" fontSize="3.5rem" />
        {loading && <p>Carregando workshops...</p>}
        {error && <p className="error">Erro: {error}</p>}

        <div className="carousel-container">
          <button className="carousel-btn left" onClick={() => scrollCarousel("left")}>&#10094;</button>
          <div className="workshop-carousel" ref={carouselRef}>
            {Array.isArray(workshops) && workshops.length > 0 ? (
              workshops.map((workshop, index) => (
                <Link to={`/workshop/${workshop.id}`} key={index} style={{ textDecoration: 'none', color: 'inherit' }}>
                  <WorkshopCard
                    text={workshop.name}
                    icon={getWorkshopIcon(workshop.name)} 
                  />
                </Link>
              ))
            ) : (
              !loading && <p>Nenhum workshop encontrado.</p>
            )}
          </div>
          <button className="carousel-btn right" onClick={() => scrollCarousel("right")}>&#10095;</button>
        </div>
      </div>
    </div>
  );
};

export default Workshops;
