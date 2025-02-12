import React, { useEffect, useState } from "react";
import { useUser } from "../../contexts/UserContext"; 
import Header from "../../components/header/Header";
import { Link } from "react-router-dom";
import WorkshopCard from "../../components/workshopCard/WorkshopCard";
import "../../pages/professor/Workshop.css";
import { useNavigate } from "react-router-dom"; 
import Button from "../../components/button/Button";

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

const WorkshopsAluno = () => {
  const { userId } = useUser(); 
  const [workshops, setWorkshops] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const navigate = useNavigate();

  useEffect(() => {
    const storedUserId = localStorage.getItem("studentId");
    if (userId) {
      localStorage.setItem("studentId", userId);
    }

    const fetchWorkshops = async () => {
      try {
        const response = await fetch(`http://localhost:5000/api/WorkshopStudent/${storedUserId || userId}/workshops`);
        console.log('API Response:', response); 
        if (!response.ok) throw new Error("Erro ao carregar workshops");

        const data = await response.json();
        console.log('Workshops Data:', data);

        if (data.workshops && Array.isArray(data.workshops)) {
          setWorkshops(data.workshops);
        } else {
          setWorkshops([]);
        }
      } catch (err) {
        console.error('Erro ao buscar workshops:', err);
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    if (storedUserId || userId) {
      fetchWorkshops();
    }
  }, [userId]);

  return (
    <div className="page">
      <Header title="SAIR" />
      <div className="inputs" >
          <Button text="MEUS WORKSHOPS" onClick={() => {}}/>
      </div>
      <div className="content">
        {loading && <p>Carregando workshops...</p>}
        {error && <p className="error">Erro: {error}</p>}

        <div className="workshop-cards">
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
      </div>
    </div>
  );
};

export default WorkshopsAluno;