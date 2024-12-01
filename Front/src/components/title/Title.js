import React from "react";
import "./Title.css";

const Title = ({ text, fontSize = "2rem" }) => {
  const titleStyle = { fontSize };

  return (
    <h1 className="title" style={titleStyle}>
      <span className="special-char">&gt;</span>
      <span className="rest-text">{text}</span>
    </h1>
  );
};

export default Title;

