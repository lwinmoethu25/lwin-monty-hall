import { useEffect, useState } from "react";
import { CrudProps } from "../models/crudProps";
import { MontyHall } from "../models/montyhall";
import "../index.css";
import {
  Button,
  Card,
  Container,
  Divider,
  Form,
  Header,
  Input,
  Loader,
  Message,
  Radio,
  Statistic,
  Transition,
} from "semantic-ui-react";

const MontyHallProblem = (props: CrudProps<MontyHall>) => {
  const [montyHall, setMontyHall] = useState<MontyHall | null>();
  const [simulationCounts, setSimulationCounts] = useState<string>("");
  const [isDoorChanged, setIsDoorChanged] = useState<boolean>(true);
  const [loading, setLoading] = useState<boolean>(false);
  const [networkError, setNetworkError] = useState<boolean>(false);

  const createSimulation = () => {
    setLoading(true);
    props
      .create("/Simulate", {
        simulationCounts,
        isDoorChanged,
      })
      .subscribe({next: res => {
        setMontyHall(res);   
      },
      error: error => {
        if (error.message === 'Network Error') {
          setNetworkError(true);
          setMontyHall(null);
        }
      }});
      resetSimulation();
  };

  const resetSimulation = () => {
    setSimulationCounts("");
    // setIsDoorChanged(true);
    setLoading(false);
    setNetworkError(false);
  };

  useEffect(() => {});

  return (
    <>
      <Container textAlign="center" className="container">
        <Header as="h1" className="header">
          Monty Hall Problem
        </Header>

        <Form size="huge" className="mt-32">
          <Form.Group inline>
            <Form.Field>
              <label>Number of Simulations</label>
              <Input
                placeholder="1000"
                type="number"
                onChange={(e) => setSimulationCounts(e.target.value)}
                value={simulationCounts}
              />
            </Form.Field>

            <Form.Field inline>
              <label>Change Door ?</label>
            </Form.Field>
            <Form.Field>
              <Radio
                label="Yes"
                name="radioGroup"
                value="true"
                defaultChecked
                checked={isDoorChanged === true}
                onChange={(e) => setIsDoorChanged(true)}
              />
            </Form.Field>
            <Form.Field>
              <Radio
                label="No"
                name="radioGroup"
                value="false"
                checked={isDoorChanged === false}
                onChange={(e) => setIsDoorChanged(false)}
              />
            </Form.Field>
          </Form.Group>
        </Form>

        <Button
          secondary
          size="huge"
          className="mt-32"
          disabled={simulationCounts.length === 0}
          onClick={() => {
            createSimulation();
          }}
        >
          Simulate
        </Button>

        {loading === true && <Loader active inline='centered' className="mt-32"/>}

        <Message negative className="mt-32" hidden={!networkError}>
          <Message.Header className="error-header">
            Error connecting to Backend API, Please enable and try again!
          </Message.Header>
        </Message>

        <Transition
          visible={montyHall ? true : false}
          animation="scale"
          duration={500}
        >
          <Container>
            <Divider horizontal className="mt-32">
              <Header as="h1" className="sub-header">
                Result
              </Header>
            </Divider>

            <Card centered className="mt-32">
              <Card.Content>
                <Card.Header className="sub-header">
                  Total Simulations :
                </Card.Header>
                <Card.Meta className="total-count">
                  {montyHall?.simulationCounts}
                </Card.Meta>
                <Divider />
                <Card.Description textAlign="center">
                  <Statistic.Group widths="two" size="small">
                    <Statistic color="green">
                      <Statistic.Value>{montyHall?.winCounts}</Statistic.Value>
                      <Statistic.Label>Win</Statistic.Label>
                    </Statistic>
                    <Statistic color="red">
                      <Statistic.Value>{montyHall?.lossCounts}</Statistic.Value>
                      <Statistic.Label>Loss</Statistic.Label>
                    </Statistic>
                  </Statistic.Group>
                </Card.Description>
              </Card.Content>
            </Card>
          </Container>
        </Transition>
      </Container>
    </>
  );
};

export default MontyHallProblem;
