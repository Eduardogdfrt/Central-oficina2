import React from 'react';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';

const TabelaCertificado = ({ students, setSelectedStudentId }) => {
  return (
    <TableContainer component={Paper} style={{ width: "100%", overflowX: "auto" }}>
      <Table style={{ minWidth: 650 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>ID do Aluno</TableCell>
            <TableCell>Nome do Aluno</TableCell>
            <TableCell>Selecionar</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {students.length > 0 ? (
            students.map((student) => (
              <TableRow key={student.id}>
                <TableCell>{student.id}</TableCell>
                <TableCell>{student.name}</TableCell>
                <TableCell>
                  <input
                    type="radio"
                    name="selectedStudent"
                    value={student.id}
                    onChange={() => setSelectedStudentId(student.id)}
                  />
                </TableCell>
              </TableRow>
            ))
          ) : (
            <TableRow>
              <TableCell colSpan={3} style={{ color: "black", fontSize: "2rem", textAlign: "center" }}>
                NÃO HÁ ALUNOS NO WORKSHOP
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default TabelaCertificado;