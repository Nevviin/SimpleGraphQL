# SimpleGraphQL
SimpleGraphQL .Net5 Web Api  using  (AutoRegisteringObjectGraphType/AutoRegisteringInputObjectGraphType) to auto-generate a graph type from a class


![image](https://user-images.githubusercontent.com/11384742/145705794-213e0730-0d4c-4ab3-896f-39e0ada66a9b.png)


# Query 

query($userDetailsInput: UserDetailsInputModel!){
  getUserDetails(userDetailsInput:$userDetailsInput){
    postCode,
    userName,
    city
  }
}


# Query Variable s

{
  "userDetailsInput": {
    "id": 1
  }
}
