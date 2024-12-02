import React, {useState} from "react";
<<<<<<< Updated upstream
=======
import { useNavigate } from 'react-router-dom';
>>>>>>> Stashed changes
import Title from "../../components/title/Title";
import Header from "../../components/header/Header";
import Input from "../../components/input/Input";
import Button from "../../components/button/Button";
import ImageButton from "../../components/image-button/ImageButton";
import "../../pages/professor/Workshop.css"

const WorkshopCadastro = () => {
    const [name, setName] = useState('');
<<<<<<< Updated upstream
=======
    const [descricao, setDescricao] = useState('');
>>>>>>> Stashed changes
    const [selectedImage, setSelectedImage] = useState(null);

    const handleImageSelect = (image) => {
      setSelectedImage(image);
      console.log("Imagem selecionada:", image);
    };

<<<<<<< Updated upstream
=======
    const navigate = useNavigate(); 

    const handleClick = () => {

        navigate('/workshops');
    };

>>>>>>> Stashed changes
    return(
        <div className="page">
        <Header title="SAIR"/>
        <div className="content">
          <Title text="NOVO WORKSHOP" fontSize="3.5rem" />
          <div className="inputs">
            <p className="text">Nome</p>
            <Input
            type="name"
            placeholder=""
            value={name}
            onChange={(e) => setName(e.target.value)}
            customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
            />
            <p className="text">Descrição</p>
            <Input
            type="name"
            placeholder=""
<<<<<<< Updated upstream
            value={name}
            onChange={(e) => setName(e.target.value)}
=======
            value={descricao}
            onChange={(e) => setDescricao(e.target.value)}
>>>>>>> Stashed changes
            customStyle={{ marginBottom: '10px', padding: '10px', fontSize: '16px' }}
            />
            <p className="text">Ícone</p>
            <ImageButton/>
        </div>
<<<<<<< Updated upstream
        <Button text="Salvar"/>
=======
        <Button text="Salvar" onClick={handleClick}/>
>>>>>>> Stashed changes
        </div>
        </div>
    );
};

export default WorkshopCadastro;