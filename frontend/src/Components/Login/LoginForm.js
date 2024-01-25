import React, { useState } from "react";
import styled from "styled-components";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useGlobalContext } from "../../context/globalContext";
import Button from "../Button/Button";
import { plus } from "../../utils/Icons";
import { dateFormat2 } from "../../utils/dateFormat";

function LoginForm() {
  const { login, error, setError } = useGlobalContext();
  const [inputState, setInputState] = useState({
    email: "",
    password: "",
  });

  const { email, password } = inputState;

  const handleInput = (name) => (e) => {
    setInputState({ ...inputState, [name]: e.target.value });
    setError("");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    login(inputState);

    setInputState({
      email: "",
      password: "",
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
          type="password"
          value={password}
          name={"title"}
          placeholder="Wpisz Haslo"
          onChange={handleInput("password")}
        />
      </div>

      <div className="submit-btn">
        <Button
          name={"Zaloguj"}
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
    border: 2px solid #fff;
    background: transparent;
    resize: none;
    box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
    color: var(--white-color);
    &::placeholder {
      color: var(--white-color);
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
      // color: rgba(34, 34, 96, 0.4);
      &:focus,
      &:active {
        // color: var(--green1);
        background: var(--dark-color2);
      }
    }
  }

  .submit-btn {
    button {
      box-shadow: 0px 1px 15px rgba(0, 0, 0, 0.06);
      background: var(--green1) !important;
      &:hover {
        background: var(--dark-color3) !important;
        // color: var(--dark-color1) !important;
      }
    }
  }
`;
export default LoginForm;
