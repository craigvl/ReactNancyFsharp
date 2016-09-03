import React from 'react';

class EquationComponent extends React.Component {


  constructor(props) {
    super(props);          
    this.state = {result : null, outcome : null, message : '', equation : '', level : '', isValid : false};
    this.onSubmit = this.onSubmit.bind(this);
    this.onInputChange = this.onInputChange.bind(this);
  }
    onInputChange(event) {
        var input = event.target.value.substr(0, 255);
        this.setState({ equation: input, isValid : input!=null && input.length > 0});// ({value: event.target.value.substr(0, 140)});
    }

    onSubmit () {       
        var level = "alert alert-";

        $.ajax({
            method : "POST",
            url: this.props.equationApiUrl,
            data : {
                equation : this.state.equation.toLowerCase(), 
                variableName : this.props.variable.toLowerCase()
            }
        }).then(res => {
            var message = "Equation resolved, ";
            switch (res.outcome) {
            case "onesolution":
                level += "success";
                message += `One solution found for ${res.variableName} : ${res.result}`;
                break;

            case "infinitesolutions":
                level += "warning";
                message += `Infinite solutions found for ${res.variableName}`;
                break;

            case "nosolution":
                level += "warning";
                message += `No solution found for ${res.variable}`;
                break;
            default:
            }
            this.setState({ message: message, level: level });
        }, res => {
            // parsing error, invalid input error
            if (res.status === 400) {
                var error = res.responseJSON;
                this.setState({
                    message: `Error while resolving the equation: '${error.equation}' for ${error.variableName}. ${error.error}`,
                    level: level + "danger"
                });
            } else {
                // server crash
                this.setState({
                    message: `Server error: '${res.statusText}'`,
                    level: level + "danger"
                });
            }


        });
  }

    render() {
        return (   
         <div className="form-group">
         <div className="input-group">
          <input type="text" className="form-control" value={this.state.equation} onChange={this.onInputChange} placeholder="Equation" />
          <span className="input-group-btn">
            <button className="btn btn-default" type="button" onClick={this.onSubmit} disabled={!this.state.equation}>Resolve {this.props.variable} </button>
          </span>
        </div>
        <div className={this.state.level}>           
            {this.state.message}
        </div>
        </div>
        );
    };

}

export default EquationComponent;
