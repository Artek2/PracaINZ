import React, { useState } from "react";
import styled from "styled-components";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { useGlobalContext } from "../../context/globalContext";
import Button from "../Button/Button";
import { plus } from "../../utils/Icons";
import { dateFormat2 } from "../../utils/dateFormat";

function ExpenseForm() {
  const { addExpense, error, setError } = useGlobalContext();
  const [inputState, setInputState] = useState({
    title: "",
    amount: 0,
    date: "",
    category: "",
    description: "",
  });

  const { title, amount, date, category, description } = inputState;

  const handleInput = (name) => (e) => {
    setInputState({ ...inputState, [name]: e.target.value });
    setError("");
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    inputState.date = dateFormat2(inputState.date);

    addExpense(inputState);
    setInputState({
      title: "",
      amount: 0,
      date: "",
      category: "",
      description: "",
    });
  };

  return (
    <ExpenseFormStyled onSubmit={handleSubmit}>
      {error && <p className="error">{error}</p>}
      <div className="input-control">
        <input
          type="text"
          value={title}
          name={"title"}
          placeholder="Wpisz Tytuł"
          onChange={handleInput("title")}
        />
      </div>
      <div className="input-control">
        <input
          value={amount}
          type="text"
          name={"amount"}
          placeholder={"Wpisz Kwotę"}
          onChange={handleInput("amount")}
        />
      </div>
      <div className="input-control">
        <DatePicker
          id="date"
          placeholderText="Wpisz Datę"
          selected={date}
          dateFormat="dd/MM/yyyy"
          onChange={(date) => {
            setInputState({ ...inputState, date: date });
          }}
        />
      </div>
      <div className="selects input-control">
        <select
          required
          value={category}
          name="category"
          id="category"
          onChange={handleInput("category")}
        >
          <option value="" disabled>
            Wybierz
          </option>
          <option value="education">Edukacja</option>
          <option value="groceries">Zakupy</option>
          <option value="health">Zdrowie</option>
          <option value="subscriptions">Subskrypcje</option>
          <option value="takeaways">Na-wynos</option>
          <option value="clothing">Ubrania</option>
          <option value="travelling">Podróże</option>
          <option value="other">Inne</option>
        </select>
      </div>
      <div className="input-control">
        <textarea
          name="description"
          value={description}
          placeholder="Opis"
          id="description"
          cols="30"
          rows="4"
          onChange={handleInput("description")}
        ></textarea>
      </div>
      <div className="submit-btn">
        <Button
          name={"Dodaj"}
          icon={plus}
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
export default ExpenseForm;
