import React, { useState } from "react";
import styled from "styled-components";
import "react-datepicker/dist/react-datepicker.css";
import { useGlobalContext } from "../../context/globalContext";
import Button from "../Button/Button";

function RegisterForm() {
  const { register, error, setError } = useGlobalContext();
  const [inputState, setInputState] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    name: "",
  });

  const { email, password, confirmPassword, name } = inputState;

  const handleInput = (name) => (e) => {
    setInputState({ ...inputState, [name]: e.target.value });
    setError("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    register(inputState);
    setInputState({
      email: "",
      password: "",
      confirmPassword: "",
      name: "",
    });
  };

  return (
    <ExpenseFormStyled onSubmit={handleSubmit}>
      {error && <p className="error">{error}</p>}
      <div className="input-control">
        <input
          type="text"
          value={email}
          name={"email"}
          placeholder="Wpisz Email"
          onChange={handleInput("email")}
        />
      </div>
      <div className="input-control">
        <input
          type="name"
          value={name}
          name={"name"}
          placeholder="Wpisz Nazwe"
          onChange={handleInput("name")}
        />
      </div>
      <div className="input-control">
        <input
          type="password"
          value={password}
          name={"password"}
          placeholder="Wpisz Haslo"
          onChange={handleInput("password")}
        />
      </div>
      <div className="input-control">
        <input
          type="password"
          value={confirmPassword}
          name={"confirmPassword"}
          placeholder="Wpisz ponowne hasło"
          onChange={handleInput("confirmPassword")}
        />
      </div>
      <div className="submit-btn">
        <Button
          name={"Zarejestruj"}
          bPad={".8rem 1.6rem"}
          bRad={"30px"}
          bg={"var(--color-accent"}
          color={"#fff"}
        />
      </div>
    </ExpenseFormStyled>
  );
}

const ExpenseFormStyled = styled.form`
  display: flex;
  flex-direction: column;
  gap: 2rem;
  input,
  textarea,
  select {
    font-family: inherit;
    font-size: inherit;
    outline: none;
    border: none;
    padding: 0.5rem 1rem;
    border-radius: 5px;
    border: 2px solid var(--bg-300);
    background: transparent;
    resize: none;
    box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
    color: var(--text-200);
    &::placeholder {
      color: var(--text-200);
    }
  }
  .input-control {
    input {
      width: 100%;
    }
  }

  .selects {
    display: flex;
    justify-content: flex-end;
    select {
      &:focus,
      &:active {
        background: var(--bg-200);
      }
    }
  }

  .submit-btn {
    button {
      box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
      background: var(--accent-200) !important;
      color: var(--text-200) !important;
      &:hover {
        background: var(--primary-100) !important;
        color: var(--text-200) !important;
      }
    }
  }
`;
export default RegisterForm;
