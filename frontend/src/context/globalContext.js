import React, { useContext, useState } from "react";
import axios from "axios";

const BASE_URL = "http://localhost:5224";
const BASE_URL_INCOME = "/Income/";
const BASE_URL_USER = "/user/";
const BASE_URL_EXPENSE = "/expense/";

const GlobalContext = React.createContext();

export const client = axios.create({
  baseURL: BASE_URL,
  headers: {
    Accept: "*/*",
    "Content-Type": "application/json",
  },
});

client.interceptors.request.use(
  function (config) {
    const token = localStorage.getItem("token");

    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    // Do something before request is sent
    return config;
  },
  function (error) {
    // Do something with request error
    return Promise.reject(error);
  }
);

export const GlobalProvider = ({ children }) => {
  const [incomes, setIncomes] = useState([]);
  const [expenses, setExpenses] = useState([]);
  const [error, setError] = useState(null);

  //calculate incomes
  const addIncome = async (income) => {
    const response = await client
      .post(`${BASE_URL_INCOME}add-income`, income)
      .catch((err) => {
        setError(err.response.data.message);
      });
    getIncomes();
  };

  const getIncomes = async () => {
    const response = await client.get(`${BASE_URL_INCOME}get-income`);
    console.log("response.data");
    setIncomes(response.data);
    console.log(response.data);
  };

  const deleteIncome = async (id) => {
    const res = await client.delete(`${BASE_URL_INCOME}delete-income/${id}`);
    getIncomes();
  };

  const totalIncome = () => {
    let totalIncome = 0;
    incomes.forEach((income) => {
      totalIncome = totalIncome + income.amount;
    });

    return totalIncome;
  };

  //calculate incomes
  const addExpense = async (income) => {
    const response = await client
      .post(`${BASE_URL_EXPENSE}add-expense`, income)
      .catch((err) => {
        setError(err.response.data.message);
      });
    getExpenses();
  };

  const getExpenses = async () => {
    const response = await client.get(`${BASE_URL_EXPENSE}get-expenses`);
    setExpenses(response.data);
    console.log(response.data);
  };

  const deleteExpense = async (id) => {
    const res = await client.delete(`${BASE_URL_EXPENSE}delete-expense/${id}`);
    getExpenses();
  };

  const totalExpenses = () => {
    let totalIncome = 0;
    expenses.forEach((income) => {
      totalIncome = totalIncome + income.amount;
    });

    return totalIncome;
  };

  const totalBalance = () => {
    return totalIncome() - totalExpenses();
  };

  const transactionHistory = () => {
    const history = [...incomes, ...expenses];
    history.sort((a, b) => {
      return new Date(b.createdAt) - new Date(a.createdAt);
    });

    return history.slice(0, 3);
  };

  const logout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("name");
    window.location.reload(false);
  };

  const login = async (user) => {
    try {
      var token = await client.post(`${BASE_URL_USER}login`, user);
      localStorage.setItem("token", token.data.token);
      localStorage.setItem("name", token.data.name);
      window.location.reload(false);
    } catch (error) {
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        alert(error.response.data);
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
        alert(error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        alert("Error", error.message);
      }
    }
  };

  const register = async (user) => {
    try {
      const response = await client.post(`${BASE_URL_USER}register`, user);
      if (response.ok) {
        const json = await response.json();
        // Handle successful registration
      } else {
        // Handle error response
      }
    } catch (error) {
      // Handle network or other errors
    }
    {
      if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        alert(error.response.data);
      } else if (error.request) {
        // The request was made but no response was received
        // `error.request` is an instance of XMLHttpRequest in the browser and an instance of
        // http.ClientRequest in node.js
        alert(error.request);
      } else {
        // Something happened in setting up the request that triggered an Error
        alert("Error", error.message);
      }
    }
  };

  const isLogin = () => {
    var token = localStorage.getItem("token");
    if (token) {
      return true;
    }
    return false;
  };

  const getName = () => {
    return localStorage.getItem("name");
  };
  return (
    <GlobalContext.Provider
      value={{
        addIncome,
        getIncomes,
        incomes,
        deleteIncome,
        expenses,
        totalIncome,
        addExpense,
        getExpenses,
        deleteExpense,
        totalExpenses,
        totalBalance,
        transactionHistory,
        error,
        setError,
        login,
        logout,
        register,
        isLogin,
        getName,
      }}
    >
      {children}
    </GlobalContext.Provider>
  );
};

export const useGlobalContext = () => {
  return useContext(GlobalContext);
};
