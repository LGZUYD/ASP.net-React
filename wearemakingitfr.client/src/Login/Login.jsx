import { useState } from "react";
import { redirect } from "../../../../node_modules/react-router-dom/dist/index"; // uhh

function Login() {
    const [invalidLogin, setInvalidLogin] = useState(false);

    const validateLogin = (event) => {
        setInvalidLogin(false);
        event.preventDefault();
        const form = event.target;
        const formData = new FormData(form);


        fetch('https://localhost:7247/users/login', {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ UserName: formData.get('userName'), Password: formData.get('password') })
        })
            .then(response => {
                if (!response.ok) {
                    setInvalidLogin(true);
                    console.log("!ok")
                    return;
                } else {
                    console.log("ok")
                    localStorage.setItem('loggedInUser', formData.get('userName'));
                    redirect("/")
                }
            }
            )
            .catch(error => console.log(error));
    }

    return (
        <div>
            <form onSubmit={validateLogin}>
                <label>
                    Enter Username
                </label><br />
                <input
                    type="text"
                    name="userName"
                />
                <label><br />
                    Enter Password
                </label><br />
                <input
                    type="text"
                    name="password"
                /><br />
                <button type="submit">Submit</button><br />
            </form>
            {invalidLogin && <div> Username or password is invalid. </div>}
        </div>
    );

}

export default Login;