const transactionValue = (transactions) => {
    var result = 0;
    console.log("Aquiiiii");
    console.log(transactions);
    for (const transaction of transactions) {
      if (transaction.type === 1 || transaction.type === 2 || transaction.type === 4) {
        result += parseInt(transaction.value);
      } else if (transaction.type === 3) {
        result -= parseInt(transaction.value);
      } else {
        return 0; //Error
      }
    }
    return result;
  };

  export {transactionValue}