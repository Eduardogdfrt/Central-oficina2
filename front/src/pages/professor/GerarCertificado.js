import React, { useState, useEffect } from "react";
import { useLocation, useNavigate } from "react-router-dom"; 
import axios from "axios";
import { useAuth } from "../../contexts/AuthContext"; 
import Title from "../../components/title/Title";
import Button from "../../components/button/Button";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";

const GerarCertificado = () => {
  const location = useLocation(); 
  const workshopId = location.state?.workshopId; 
  const { setToken } = useAuth(); 
  const [workshop, setWorkshop] = useState(null);
  const [studentId, setStudentId] = useState("");
  const [error, setError] = useState("");
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate(); 

  useEffect(() => {
    const fetchWorkshopData = async () => {
      try {
        const response = await axios.get(`https://centraloficina2-327755630538.us-central1.run.app/api/Workshop/${workshopId}`);
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

    if (workshopId) {
      fetchWorkshopData();
    } else {
      setError("Workshop ID não disponível.");
    }
  }, [workshopId]);

  const handleGenerateCertificate = async (e) => {
    e.preventDefault();
    try {
      const response = await axios.post("https://centraloficina2-327755630538.us-central1.run.app/api/WorkshopStudent/workshopStudent/emitir-certificado", {
        certificados: [
          {
            studentId,
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

  if (error) {
    return <p style={{ color: "red" }}>{error}</p>;
  }

  return (
    <div className="page">
      <Header title="Gerar Certificado" />
      <div className="content">
        <Title text={workshop.name} fontSize="3.5rem" />
        <p className="text">{new Date(workshop.data).toLocaleString()}</p>
        <div className="inputs">
          <form onSubmit={handleGenerateCertificate} className="form-container">
            <p className="text no-width">Informe o ID do aluno para gerar o certificado:</p>
            <Input 
              type="text"
              value={studentId}
              onChange={(e) => setStudentId(e.target.value)}
              placeholder="ID do aluno"
            />
            {error && <p style={{ color: "red" }}>{error}</p>}
            <Button text="Gerar Certificado" type="submit" />
          </form>
        </div>
      </div>
    </div>
  );
};

export default GerarCertificado;
