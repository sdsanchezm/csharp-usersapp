// version 2: 
import React, { useState, useEffect } from "react";
import styled from "styled-components";

// Base url - from the dotnet app
const BASE_URL = `http://localhost:5059`;

const UserFrame = styled.div`
margin: 15px 10px;
font-family: Arial;
padding: 10px;
border-radius: 5px;
border: solid 1px gray;
    box-shadow: 0 0 5px grey;
`;

const Input = styled.input`
border-radius: 3px;
padding: 5px;
border: solid 1px black;
`;

const Title = styled(Input)`
    text-transform: Capitalized;
`;

const Save = styled.button`
background: purple;
padding: 10px;
border-radius: 5px;
margin: 10px;
font-size: 16px;
color: white;
width: 100px;
`;

// User component
const User = ({ user }) => {
  const [data, setData] = useState(user);
  const [dirty, setDirty] = useState(false);

  function update(value, fieldName, obj) {
    setData({ ...obj, [fieldName]: value });
    setDirty(true);
  }

  function onSave() {
    setDirty(false);
    const idx = data.id;
    const apiUrl = `${BASE_URL}/user/${idx}`;

    console.log(data);

    fetch(apiUrl, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((updatedData) => {
        setData(updatedData);
        setDirty(false);
      })
      .catch((error) => {
        console.error('Error updating data:', error);
      });

  }

  return (<React.Fragment>
    <UserFrame>
      <h3> <Title onChange={(evt) => update(evt.target.value, 'Firstname', data)} value={data.firstname} /> </h3>
      <div> <Input onChange={(evt) => update(evt.target.value, 'firstname', data)} value={data.firstname} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'Lastname', data)} value={data.lastname} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'About', data)} value={data.about} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'username', data)} value={data.username} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'usernumber', data)} value={data.usernumber} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'city', data)} value={data.city} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'province', data)} value={data.usernumber} /> </div>
      <div> <Input onChange={(evt) => update(evt.target.value, 'country', data)} value={data.country} /> </div>
      {dirty ? <div><Save onClick={onSave}>Save</Save></div> : null}
    </UserFrame>
  </React.Fragment>)
}


// Main function
const Main = () => {
  const [users, setUsers] = useState([]);
  useEffect(() => {
    fetchData();
  }, [])

  function fetchData() {

    const apiUrl = `${BASE_URL}/user`;
    fetch(apiUrl)
      .then(response => response.json())
      .then(data => setUsers(data))
  }

  const data = users.map(user => <User user={user} />)

  return (<React.Fragment>
    {users.length === 0 ?
      <div>No users yet...</div> :
      <div>{data}</div>
    }
  </React.Fragment>)
}


export default Main;


