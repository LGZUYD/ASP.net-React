import { useState, useEffect } from 'react';
function CreateAccount() {

    const [passwordConfirm, setPasswordConfirm] = useState(false);
    const [userNameAlreadyExist, setUserNameAlreadyExist] = useState(false);
    const [accountCreated, setAccountCreated] = useState(false);

    const createAccount = (event) => {
        event.preventDefault();
        const form = event.target;
        const formData = new FormData(form);

        if (formData.get('password') != formData.get('password-confirm')) {
            setPasswordConfirm(true);
            return;
        }

        fetch(`https://localhost:7247/users/create`, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ UserName: formData.get('userName'), Password: formData.get('password') })
        })
            .then(response => {
                if (!response.ok) {
                    setUserNameAlreadyExist(true);
                    return;
                } else {
                    setAccountCreated(true);
                }
            }) 
            .catch(error => {
                console.error('Error sending data:', error);
            });

        setPasswordConfirm(false);
        setUserNameAlreadyExist(false);
    }



return (
    <>
        <form onSubmit={createAccount}>
            <label>
                Enter Username
            </label><br />
            <input
                type="text"
                name="userName"
            />    <br />

            <label>
                Enter Password
            </label><br />

            <input
                type="text"
                name="password"
            />
            <label><br />

                Confirm Password
            </label><br />
            <input
                type="text"
                name="password-confirm"
            /><br />

            <button type="submit">Submit</button><br />

        </form>
        {passwordConfirm &&
            <div> Confirmed password does not match.</div>}
        {userNameAlreadyExist &&
            <div> Username already exists.</div>}
        {accountCreated &&
            <div> New account created.</div>}

        
    </>

)



}

export default CreateAccount;