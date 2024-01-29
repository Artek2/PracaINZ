import { createGlobalStyle } from "styled-components";

export const GlobalStyle = createGlobalStyle`
    *{
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        list-style: none;
    }

    :root{
       
        // --green1:#0D6E6E;
        // --green2:#4a9d9c;
        // --green3:#afffff;
        
        // --color-neutral:#ffe0c8;
        // --dark-color1:#0D1F2D;
        // --dark-color2:#1d2e3d;
        // --dark-color3:#354656;
        // --grey-color:#e0e0e0;
        // --white-color:#FFFFFF;

        --primary-100:#eb9c64;
        --primary-200:#ff8789;
        --primary-300:#554e4f;
        --accent-100:#8fbf9f;
        --accent-200:#346145;
        --text-100:#353535;
        --text-200:#000000;
        --bg-100:#F5ECD7;
        --bg-200:#ebe2cd;
        --bg-300:#c2baa6;

        --color-orange:#FF3D3D;
          
        
    }

    body{
        font-family: 'Nunito', sans-serif;
        font-size: clamp(1rem, 1.5vw, 1.2rem);
        overflow: hidden;
        color: var(--white-color);
    }

    h1, h2, h3, h4, h5, h6{
        color: var(--text-200);
    }
    p{
        color:var(--text-100);
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
