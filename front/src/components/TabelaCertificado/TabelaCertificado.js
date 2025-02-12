import React from "react";
import { DataGrid } from "@mui/x-data-grid";
import Paper from "@mui/material/Paper";

const TabelaCertificado = ({ students, handleStudentSelection, selectedStudentIds }) => {
  const columns = [
    { field: "id", headerName: "ID do Aluno", width: 150 },
    { field: "name", headerName: "Nome do Aluno", width: 300 },
    { field: "email", headerName: "Email", width: 300 },
    { 
      field: "birthDate", 
      headerName: "Data de Nascimento", 
      width: 200, 
      valueGetter: (params) => new Date(params.value).toLocaleDateString() 
    },
  ];

  const rows = students.map(student => ({
    id: student.id,
    name: student.name,
    email: student.email,
    birthDate: student.birthDate,
  }));

  const handleSelectionChange = (newSelection) => {
    handleStudentSelection(newSelection);
  };

  return (
    <Paper sx={{ height: 500, width: "70%" }}>
      <DataGrid
        rows={rows}
        columns={columns}
        pageSizeOptions={[5, 10]}
        checkboxSelection
        onRowSelectionModelChange={(newSelection) => handleSelectionChange(newSelection)}
        rowSelectionModel={selectedStudentIds}
        sx={{ border: 0, overflowX: "hidden" }}
      />
    </Paper>
  );
};

export default TabelaCertificado;
