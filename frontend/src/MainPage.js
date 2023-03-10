import { useCallback, useState, useEffect } from "react";
import TransactionDataService from "./services/transaction.service";
import "./MainPage.css";
import { styled } from "@mui/material/styles";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell, { tableCellClasses } from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Alert from "@mui/material/Alert";
import AlertTitle from "@mui/material/AlertTitle";

const MainPage = () => {
  const [producers, setProducers] = useState([]);
  const [transactionByProducer, setTransactionByProducer] = useState([]);
  const [file, setFile] = useState([]);
  const [uploadSucess, setUploadSucess] = useState(false);
  const [error, setError] = useState(false);
  const [uploadDisable, setUploadDisable] = useState(true);

  const getAllDistinctProducers = useCallback(async () => {
    const result = await TransactionDataService.getAllSellers();
    if (result) {
      const uniqueProducers = [...new Set(result.data.map((item) => item))];
      setProducers(uniqueProducers);
    } else {
      setError("Internal Server Error, try again later.");
    }
  }, []);

  const getAllTransactionsByProducers = async (producers) => {
    getAllDistinctProducers();
    const transactionsProducers = [];
    for (const producer of producers) {
      const producerTransaction = {
        producer: producer.name,
        transaction: producer.sales,
        total: producer.amountTotal.toLocaleString('pt-br', {
          style: 'currency',
          currency: 'BRL'
        })
      };
      transactionsProducers.push(producerTransaction);
    }
    return transactionsProducers;
  };

  const getAllTransactionsByProducersFunction = async (producers) => {
    const data = await getAllTransactionsByProducers(producers);
    setTransactionByProducer(data);
  };

  useEffect(() => {
    getAllDistinctProducers();
  }, [getAllDistinctProducers]);

  const StyledTableCell = styled(TableCell)(({ theme }) => ({
    [`&.${tableCellClasses.head}`]: {
      backgroundColor: theme.palette.common.black,
      color: theme.palette.common.white
    },
    [`&.${tableCellClasses.body}`]: {
      fontSize: 14
    }
  }));

  const handleFileChange = (e) => {
    if (e.target.files) {
      setFile(e.target.files[0]);
      setUploadDisable(false);
    }
  };
  const readFile = async (event) => {
    if (file) {
      await TransactionDataService.create(file)
      .then((res) => {
        if (res) {
          setUploadSucess(true);
          getAllTransactionsByProducersFunction(producers);
        } else {
          setError("Internal Server Error, try again later.");
        }
      })
      .catch((err) => {
        setUploadSucess(false);
        setError(err.response.data || "Internal Server Error, try again later.");
      });
    }
    setFile(null);
    setUploadDisable(true);
  };

  const handleCloseAlert = () => {
    setUploadSucess(false);
  };

  const handleCloseAlertError = () => {
    setError();
  };

  return (
    <div>
      <Typography variant="h3" gutterBottom>
        Sales by Producers
      </Typography>
      <br />
      <Button variant="contained" data-testid="getAll" onClick={() => getAllTransactionsByProducersFunction(producers)}>
        Get all transactions by producers
      </Button>
      <br />
      <br />
      <Typography variant="h4" gutterBottom>
        Forms: Upload File of Transactions
      </Typography>
      <div>
        <Button data-testid="chooseFile" variant="contained" component="label">
          Choose File
          <input data-testid="fileInput" type="file" hidden onChange={handleFileChange} />
        </Button>
        <Typography data-testid="fileName" variant="p">
          {file && file.name && file.type && `${file.name} - ${file.type}`}
        </Typography>
        <br />
        <br />
        <Button data-testid="upload" variant="contained" component="label" onClick={readFile} disabled={uploadDisable}>
          Upload
        </Button>
      </div>
      {uploadSucess && (
        <Alert data-testid="alertSuccess" severity="success" onClose={() => handleCloseAlert()}>
          <AlertTitle>Success</AlertTitle>
          Your file was uploaded!
        </Alert>
      )}
      {error && (
        <Alert severity="error" onClose={() => handleCloseAlertError()}>
          <AlertTitle>Error</AlertTitle>
          {error}
        </Alert>
      )}
      {transactionByProducer &&
        transactionByProducer.map((row) => (
          <>
            <Typography variant="h4" gutterBottom>
              {row.producer}
            </Typography>
            <TableContainer component={Paper}>
              <Table sx={{ minWidth: 650, maxWidth: 800 }} aria-label="simple table">
                <TableHead>
                  <TableRow>
                    <StyledTableCell>Id</StyledTableCell>
                    <StyledTableCell align="right">Type</StyledTableCell>
                    <StyledTableCell align="right">Date</StyledTableCell>
                    <StyledTableCell align="right">Product</StyledTableCell>
                    <StyledTableCell align="right">Value</StyledTableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {row.transaction &&
                    row.transaction.map((item) => (
                      <>
                        <TableRow key={item.id} sx={{ "&:last-child td, &:last-child th": { border: 0 } }}>
                          <TableCell component="th" scope="row">
                            {item.id}
                          </TableCell>
                          <TableCell align="right">{item.saleType}</TableCell>
                          <TableCell align="right">{item.date}</TableCell>
                          <TableCell align="right">{item.description}</TableCell>
                          <TableCell align="right">{item.value.toLocaleString('pt-br', {
          style: 'currency',
          currency: 'BRL'
        })}</TableCell>
                        </TableRow>
                      </>
                    ))}
                  <Typography variant="p" gutterBottom>
                    Total: {row.total}
                  </Typography>
                </TableBody>
              </Table>
            </TableContainer>
          </>
        ))}
    </div>
  );
};

export default MainPage;
