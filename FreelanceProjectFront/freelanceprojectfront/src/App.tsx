import React from 'react';
import logo from './logo.svg';
import './App.css';

function App() {
  return (
      <div>
          <header className="App-header">
              <p>NFreelance</p>
          </header>
          <div className="App-body">
              <p className="App-body1">Log In</p>
              <form >
                  <p><input type="text" name="login" placeholder="you'r login" /></p>
                  <p><input type="password" name="password" placeholder="you'r password" /></p>
                  <p><button className="App-button">LogIn</button></p>
              </form>
          </div>
    </div>
  );
}

export default App;
