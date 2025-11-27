import { useState, useEffect } from 'react';
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'



function App() {
  const [data, setData] = useState(null);

  useEffect(() => {
    fetch(`http://localhost:8080/api/train`)
      .then(res => res.json())
      .then(data => {
        console.log(data['data']);
        setData(data);
      })
      .catch(err => console.error(err));
  }, []); // пустой массив = вызов только один раз при монтировании

  const [count, setCount] = useState(0);

  return (
    <>
      <h1>Vite + React</h1>
      <button onClick={() => setCount(count + 1)}>count is {count}</button>
      <pre>{data && JSON.stringify(data, null, 2)}</pre>
    </>
  );
}


export default App
