import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom"; 
import axios from "axios";
import { useAuth } from "../../contexts/AuthContext"; 
import Title from "../../components/title/Title";
import Button from "../../components/button/Button";
import Header from "../../components/header/Header";
import TabelaCertificado from "../../components/TabelaCertificado/TabelaCertificado";

const GerarCertificado = () => {
  const location = useLocation(); 
  const workshopId = location.state?.workshopId; 
  const { setToken } = useAuth(); 
  const [workshop, setWorkshop] = useState(null);
  const [students, setStudents] = useState([]);
  const [selectedStudentId, setSelectedStudentId] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate(); 

  const API_BASE_URL = process.env.DSV;

  useEffect(() => {
    const fetchWorkshopData = async () => {
      try {
        const response = await axios.get(`http://localhost:5000/api/Workshop/${workshopId}`);
        if (response.status === 200) {
          setWorkshop(response.data.workshop);
        } else {
          setError("Workshop não encontrado.");
        }
      } catch (err) {
        setError("Erro ao carregar os dados do workshop.");
      } finally {
        setLoading(false);
      }
    };

    const fetchStudents = async () => {
      try {
        const response = await axios.get(`http://localhost:5000/api/WorkshopStudent/workshop/${workshopId}/students`);
        if (response.status === 200) {
          setStudents(response.data.students);
        } else {
          setError("Alunos não encontrados.");
        }
      } catch (err) {
        setError("Erro ao carregar os dados dos alunos.");
      }
    };

    if (workshopId) {
      fetchWorkshopData();
      fetchStudents();
    } else {
      setError("Workshop ID não disponível.");
      setLoading(false);
    }
  }, [workshopId, API_BASE_URL]);

  const handleGenerateCertificate = async () => {
    try {
      const response = await axios.post(`http://localhost:5000/api/WorkshopStudent/workshopStudent/emitir-certificado`, {
        certificados: [
          {
            studentId: selectedStudentId,
            workshopId: workshopId
          }
        ]
      });

      if (response.status === 200) {
        const generatedToken = response.data.token; 
        setToken(generatedToken); 
        navigate("/gerar-certificado", { state: { token: generatedToken } });
      } else {
        setError("Falha ao gerar certificado.");
      }
    } catch (err) {
      setError("Erro ao gerar certificado.");
    }
  };

  if (loading) {
    return <p>Carregando dados do workshop...</p>;
  }

  return (
    <div className="page" style={{ width: "100vw", height: "100vh", margi: 0}}>
      <Header title="Gerar Certificado" />
      <div style={{ display: "flex", flexDirection: "column", alignItems: "center"}}>
        <Title text={workshop?.name } fontSize="3.5rem" style={{ margin: 0 }} />
        <p className="text">{workshop ? new Date(workshop.data).toLocaleString() : ""}</p>
        <p className="text no-width" style={{ whiteSpace: "nowrap", margin: 0 }}>Selecione os alunos para gerar os certificado:</p>
        <div style={{display:'flex', alignItems : 'flex-start', justifyContent:'space-between'}}>
          <div style={{marginRight: "5vh", marginBottom:"20px"}}>
            <Button text="Gerar Certificado" onClick={handleGenerateCertificate} disabled={!selectedStudentId} style={{ marginRight: "30px", width: "10vh" }} />
          </div>
          <div>
            <Button text="Voltar para Workshops" onClick={() => navigate("/workshops")} style={{ width: "10vh" }} />
          </div>
        </div>
        <div style={{ width: "70vw", overflowY: "auto", display: "flex", justifyContent: "center" }}>
          <TabelaCertificado students={students} setSelectedStudentId={setSelectedStudentId} />
        </div>
      </div>
    </div>
  );
};

export default GerarCertificado;