import React, { useState } from "react";
import "./ImageButton.css"; 

const ImageButton = () => {
  const [fileName, setFileName] = useState("");

  const handleFileChange = (event) => {
    const file = event.target.files[0];
    if (file) {
      setFileName(file.name);
    }
  };

  return (
    <div className="file-upload-container">
      <input
        type="file"
        id="file-input"
        className="file-input"
        onChange={handleFileChange}
        style={{ display: "none" }} 
      />
      <input
        type="text"
        readOnly
        value={fileName}
        className="file-name-display"
        placeholder="Nenhum arquivo selecionado"
      />
      <button className="upload-button" onClick={() => document.getElementById('file-input').click()}>
        Carregar
      </button>
    </div>
  );
};

export default ImageButton;
