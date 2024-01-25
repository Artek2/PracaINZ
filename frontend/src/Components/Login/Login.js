import React, { useEffect } from "react";
import styled from "styled-components";
import { useGlobalContext } from "../../context/globalContext";
import { InnerLayout } from "../../styles/Layouts";
// import Form from "../Form/Form";
import LoginForm from "./LoginForm";

function Login() {
  return (
    <ExpenseStyled>
      <InnerLayout>
        <h1>Zaloguj się</h1>
        <div className="income-content">
          <div className="form-container">
            <LoginForm />
          </div>
        </div>
      </InnerLayout>
    </ExpenseStyled>
  );
}

const ExpenseStyled = styled.div`
  display: flex;
  overflow: auto;
  .income-content {
    display: flex;
    gap: 2rem;
    .incomes {
      flex: 1;
    }
  }
`;

export default Login;
