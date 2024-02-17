import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import { v4 as uuidv4 } from 'uuid';

interface ICreateMortgageState {
    applicationId: string;
    mortgageAmount: number;
    amountLefter: number;
}

interface CreateMortgageProps {
    id?: string;
}

class MortgageSection {
    id: string = "";
    mortgageAmount: number = 0;
}

const CreateMortgage: React.FC = ({ }) => {
    // const [applicationId, setApplicationId] = useState<string>(id);
    const [mortgageAmount, setMortgageAmount] = useState<number>(200000);
    const [mortgageAmountLeft, setMortgageAmountLeft] = useState<number>(200000) ;
    const [sections, setSections] = useState<MortgageSection[]>([]);


    const [isFormValid, setIsFormValid] = useState<boolean>(false);
    let { id } = useParams();
    
    const [validated, setValidated] = useState(false);



    useEffect(() => {
        // This code will run every time `count` changes
            setIsFormValid(mortgageAmountLeft == 0);
    }, [mortgageAmountLeft]);
    
    useEffect(() => {
        // This code will run every time `count` changes
        setMortgageAmountLeft( sections.reduce((a, b) => a - b.mortgageAmount, mortgageAmount))
    }, [sections]);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        const form = event.currentTarget;
        if (form.checkValidity() === false || !isFormValid) {
            event.preventDefault();
            event.stopPropagation();
        }

        setValidated(true);
    };



    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
            // setApplicationId(event.target.value);
    };

    const handleCreateMortgage = () => {
        // Logic to create a mortgage with the application amount
        // ...
    };

    const handleCancel = () => {
        // Logic to cancel the mortgage creation
        // ...
    };

    const handleGoBack = () => {
        // Logic to navigate back to the list of mortgages
        // ...
    };

    const handleRemoveSection = (id: string) => {
        // Logic to navigate back to the list of mortgages
        // ...

        sections.splice(sections.findIndex((section) => section.id === id), 1);
        setSections([...sections]);
    };

    const handleAddSection = () => {
        const newSection = new MortgageSection();
        let myuuid = uuidv4();

        newSection.id = myuuid.toString();
        newSection.mortgageAmount = mortgageAmountLeft;
        setSections([...sections, newSection]);
    };


    const handleAmountChange = (newAmount: string, sectionId: string) => {
        // Convert the new amount to a number
        const amount = Number(newAmount);

        // Find the section with the given ID and update its amount
        const newSections = sections.map((section) => {
            if (section.id === sectionId) {
                return { ...section, mortgageAmount: amount };
            } else {
                return section;
            }
        });

        // Update the state
        setSections(newSections);
        

    };

    // Print the application id when the component is rendered
    console.log("applicationToUse: ", id);

    return (
        <div>
            <h2>Create Mortgage</h2>
            <p>Application ID: {id}</p>
            <p className={isFormValid ? '' : 'invalid-text'} >Amount left: {mortgageAmountLeft}</p>


            <form>
                <label htmlFor="mortgageAmount">Mortgage Amount:</label>
                <input
                    type="number"
                    id="mortgageAmount"
                    value={mortgageAmount}
                    onChange={(event) => setMortgageAmount(Number(event.target.value))}
                />
                <button type="submit" onClick={handleCreateMortgage}>
                    Create Mortgage
                </button>
                <button type="button" onClick={handleAddSection}>
                    Add Section
                </button>
                <button type="button" onClick={handleCancel}>
                    Cancel
                </button>
                <button type="button" onClick={handleGoBack}>
                    Go Back
                </button>
            </form>
            <Form noValidate validated={validated} onSubmit={handleSubmit} >

                <table>
                    <thead>
                        <tr>
                            <th>Section ID</th>
                            <th>Mortgage Amount</th>
                        </tr>
                    </thead>
                    <tbody>

                        {sections.map((section) => (
                            <tr key={section.id}>
                                <td>{section.id}</td>
                                <td>{section.mortgageAmount}</td>
                                <td>

                                    <Form.Group className="mb-3" controlId="formBasicAmount">
                                        <Form.Label>Amount</Form.Label>
                                        <Form.Control type="number" placeholder="amount" onChange={(event) => handleAmountChange(event.target.value, section.id)} required />
                                    </Form.Group>

                                </td>
                                <td><button type='button' onClick={() => handleRemoveSection(section.id)} >-</button></td>
                            </tr>
                        ))}

                    </tbody>
                </table>

                <Button type="submit">Create mortgage</Button>
            </Form>

        </div >
    );
};

export default CreateMortgage;




