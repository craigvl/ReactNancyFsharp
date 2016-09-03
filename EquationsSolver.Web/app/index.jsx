import React from 'react';
import {render} from 'react-dom';
import EquationComponent from './equationComponent.jsx';

class App extends React.Component {

    constructor(props) {
        super(props);
        //this.props.variable = "x";
        //this.props.equationApiUrl = "http://localhost:12321/resolve/";
    }
    render () { return (
        <div>
            <form className="col-sm-4">       
                <h1> Equation Resolver </h1>
                <div>
                <span id="helpBlock" className="help-block">
                    <p> Only one variable can resolved, which is  <strong> {this.props.variable}  </strong> </p>
                    <p> Equation must be linear, <em>x^2</em> is not allowed </p>
                    <p> Operands must be clearly defined i.e. <em>2x=1</em> is not allowed, must be <em>2*x=1 </em></p>
                    <p> Web Api must be running on  <strong> {this.props.variable} </strong> </p>
                </span>     
                </div>
         
                <EquationComponent variable={this.props.variable} equationApiUrl={this.props.equationApiUrl}> </EquationComponent>
          
            </form>    
        </div> 
    ); }
}

App.propTypes = {
    equationApiUrl: React.PropTypes.string.isRequired,
    variable: React.PropTypes.string.isRequired
}

// app config, could be xtracted into a separate json file
App.defaultProps = {
    equationApiUrl:  "http://localhost:12321/resolve/" ,
    variable:"x"
};

render(<App/>, document.getElementById('app'));