import React, { useEffect } from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'
import './index.css'
import LoginPage from './Login/LoginPage.jsx'
//import Message from './Message.jsx'

import {
    createBrowserRouter,
    RouterProvider,
} from "react-router-dom";

let loggedIn = localStorage.getItem('loggedInUser')
let startUpPage = loggedIn ? <App /> : <LoginPage />;

const router = createBrowserRouter([    
    {
        path: "/",
        element: startUpPage,
    },
    {
        path: "/login",
        element: <LoginPage/>,
    },
]);

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <RouterProvider router={router} />
    </React.StrictMode>,
)
