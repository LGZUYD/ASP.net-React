import { useEffect, useState } from 'react';
import './Login.css';
import CreateAccount from './CreateAccount.jsx'
import Login from './Login.jsx'

function LoginPage() {
    const [showCreateAccount, setShowCreateAccount] = useState(false);
    const [showLogin, setLogin] = useState(false);

    const handleCreateAccountClick = () => {
        setShowCreateAccount(!showCreateAccount); 
        setLogin(false); 
    }

    const handleLoginClick = () => {
        setLogin(!showLogin); 
        setShowCreateAccount(false); 
    }


    return (
        <>
            <div className="container">
                <button id="login" onClick={handleLoginClick}>Login</button>
                {!showCreateAccount && showLogin && <Login/>}
                <button id="create-account" onClick={handleCreateAccountClick}>Create New Account</button>
                {showCreateAccount && !showLogin && <CreateAccount/> }
            </div>
        </>
    )

}

export default LoginPage;