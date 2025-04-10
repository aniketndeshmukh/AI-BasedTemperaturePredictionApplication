import pandas as pd
import numpy as np
from sklearn.linear_model import LinearRegression
import joblib

# Sample training data (replace with real data)
data = {
    'Days': [1, 2, 3, 4, 5, 6, 7],
    'Temperature': [30, 32, 31, 33, 34, 35, 36]
}

df = pd.DataFrame(data)

# Train model
model = LinearRegression()
model.fit(df[['Days']], df['Temperature'])

# Save the model
joblib.dump(model, 'temperature_model.pkl')
