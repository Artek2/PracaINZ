import { createGlobalStyle } from "styled-components";

export const GlobalStyle = createGlobalStyle`
    *{
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        list-style: none;
    }

    :root{
        // --primary-color: #FFFFFF;
        // --primary-color2: 'color: rgba(34, 34, 96, .6)';
        // --primary-color3: 'color: rgba(34, 34, 96, .4)';
        // --color-green: #42AD00;
        // --color-grey: #aaa;
        // --color-accent: #F56692;
        // --color-delete: #FF0000;


       


        --green1:#0D6E6E;
        --green2:#4a9d9c;
        --green3:#afffff;
        --color-orange:#FF3D3D;
        --color-neutral:#ffe0c8;
        --dark-color1:#0D1F2D;
        --dark-color2:#1d2e3d;
        --dark-color3:#354656;
        --grey-color:#e0e0e0;
        --white-color:#FFFFFF;
        
    }

    body{
        font-family: 'Nunito', sans-serif;
        font-size: clamp(1rem, 1.5vw, 1.2rem);
        overflow: hidden;
        color: var(--white-color);
    }

    h1, h2, h3, h4, h5, h6{
        color: var(--white-color);
    }

    .error{
        color: red;
        animation: shake 0.5s ease-in-out;
        @keyframes shake {
            0%{
                transform: translateX(0);
            }
            25%{
                transform: translateX(10px);
            }
            50%{
                transform: translateX(-10px);
            }
            75%{
                transform: translateX(10px);
            }
            100%{
                transform: translateX(0);
            }
        }
    }
`;
