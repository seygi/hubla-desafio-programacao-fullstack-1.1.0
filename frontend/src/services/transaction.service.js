import http from "../Http-common";

class TransactionDataService {

  
  create(file) {
    const formData = new FormData();
    formData.append("file", file);
    return http.post("/Sales", formData, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    }).catch(function (error) {
        console.log(JSON.stringify(error))
    });
  }

  getAllSellers() {
    return http.get("/Sellers").catch(function (error) {
        console.log(JSON.stringify(error.message))
    });;
  }
}

export default new TransactionDataService();