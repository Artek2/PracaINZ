import { useRef, useState, useEffect } from "react";
import styled from "styled-components";
import "react-datepicker/dist/react-datepicker.css";
import { useGlobalContext } from "../../context/globalContext";
import Button from "../Button/Button";

const USER_REGEX = /^[A-z][A-z0-9-_]{3,23}$/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
const MAIL_REGEX = /^[A-z0-9._%+-]+@[A-z0-9.-]+\.[A-z]{2,4}$/;

function RegisterForm() {
  const { register, error, setError } = useGlobalContext();
  const [inputState, setInputState] = useState({
    email: "",
    password: "",
    confirmPassword: "",
    name: "",
  });

  //walidacja
  const [emailError, setEmailError] = useState("");
  const [nameError, setNameError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [confirmPasswordError, setConfirmPasswordError] = useState("");

  const { email, password, confirmPassword, name } = inputState;

  const handleInput = (name) => (e) => {
    setInputState({ ...inputState, [name]: e.target.value });
    switch (name) {
      case "email":
        validateEmail(e.target.value);
        break;
      case "name":
        validateName(e.target.value);
        break;
      case "password":
        validatePassword(e.target.value);
        break;
      case "confirmPassword":
        validateConfirmPassword(e.target.value);
        break;
      default:
        break;
    }
    setError("");
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!emailError && !nameError && !passwordError && !confirmPasswordError) {
      register(inputState);
      setInputState({
        email: "",
        password: "",
        confirmPassword: "",
        name: "",
      });
    } else {
      setError("Wypełnij poprawnie wszystkie pola.");
    }
  };

  //walidacja

  
  const validateEmail = (value) => {
    if (!MAIL_REGEX.test(value)) {
      setEmailError("Nieprawidłowy adres email.");
    } else {
      setEmailError("");
    }
  };

  const validateName = (value) => {
    if (!USER_REGEX.test(value)) {
      setNameError(
        "Nazwa musi składać się z od 4 do 24 znaków. Musi zaczynać się od litery. Dozwolone litery, cyfry, podkreślnik, myślnik."
      );
    } else {
      setNameError("");
    }
  };

  const validatePassword = (value) => {
    if (!PWD_REGEX.test(value)) {
      setPasswordError(
        "Hasło musi zawierać conajmniej 8 znaków, małą i wielką literę, cyfrę i znak specjalny."
      );
    } else {
      setPasswordError("");
    }
  };

  const validateConfirmPassword = (value) => {
    if (value !== password) {
      setConfirmPasswordError("Hasła muszą być identyczne.");
    } else {
      setConfirmPasswordError("");
    }
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
        {emailError && <p className="error">{emailError}</p>}
      </div>
      <div className="input-control">
        <input
          type="name"
          value={name}
          name={"name"}
          placeholder="Wpisz Nazwe"
          onChange={handleInput("name")}
        />
        {nameError && <p className="error">{nameError}</p>}
      </div>
      <div className="input-control">
        <input
          type="password"
          value={password}
          name={"password"}
          placeholder="Wpisz Haslo"
          onChange={handleInput("password")}
        />
        {passwordError && <p className="error">{passwordError}</p>}
      </div>
      <div className="input-control">
        <input
          type="password"
          value={confirmPassword}
          name={"confirmPassword"}
          placeholder="Wpisz ponowne hasło"
          onChange={handleInput("confirmPassword")}
        />
        {confirmPasswordError && (
          <p className="error">{confirmPasswordError}</p>
        )}
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
      max-width: 260px;
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

  .error {
    display: block;
    border: 2px solid var(--error-color);
    border-radius: 5px;
    padding: 1rem;
    max-width: 350px;
    margin-top: 15px;
    color: var(--text-200);
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
